using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TickdMeterReading.Application.ViewModels;
using TickdMeterReading.Domain.Accounts.ValueObjects;
using TickdMeterReading.Domain.MeterReadings;
using TickdMeterReading.Domain.MeterReadings.ValueObjects;

namespace TickdMeterReading.API.Extensions
{
    public static class CsvValidator
    {
        public static async Task<List<MeterReadingViewModel>> ValidateCsvUploadAsync(IWebHostEnvironment env, IFormFile csvFile, List<AccountViewModel> validAccounts)
        {
            string uploads = Path.Combine(env.ContentRootPath, "TemporaryStore");
            string filePath = "";

            if (csvFile.FileName.EndsWith(".csv"))
            {
                try
                {
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    filePath = Path.Combine(uploads, csvFile.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await csvFile.CopyToAsync(fileStream);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error in ValidateCsvUploadAsync");
                    return null;
                }

                List<MeterReadingViewModel> meterReadings = new List<MeterReadingViewModel>();
                int failedResults = 0;

                using (var reader = new StreamReader(filePath))
                {
                    string[] headers = reader.ReadLine().Split(",");
                    while (!reader.EndOfStream)
                    {
                        string[] row = reader.ReadLine().Split(",");
                        DateTime dateTime;
                        try
                        {
                            dateTime = DateTime.Parse(row[1]);
                        }
                        catch
                        {
                            failedResults++;
                            continue;
                        }

                        Regex rx = new Regex(@"\d{5}");
                        if (!rx.IsMatch(row[2]))
                        {
                            failedResults++;
                            continue;
                        }

                        int outValue = 0;
                        bool isInt = int.TryParse(row[2], out outValue);
                        if (!isInt || outValue < 0)
                        {
                            failedResults++;
                            continue;
                        }

                        int outId = 0;
                        isInt = int.TryParse(row[0], out outId);
                        if (!isInt || outId < 0)
                        {
                            failedResults++;
                            continue;
                        }

                        var doubleCheck = meterReadings.Where(e => e.AccountId == outId).OrderBy(e => e.MeterReadingDateTime).ToArray();

                        if (doubleCheck.Length > 0 && (doubleCheck.Last().MeterReadValue == outValue.ToString() || doubleCheck.Last().MeterReadingDateTime >= dateTime))
                        {
                            failedResults++;
                            continue;
                        }

                        meterReadings.Add(new MeterReadingViewModel(null, new AccountId(outId), new MeterReadingDateTime(dateTime), new MeterReadValue(outValue.ToString())));
                    }

                    var validAccountIds = validAccounts.Select(a => a.AccountId).ToList();

                    failedResults += meterReadings.Where(a => !validAccountIds.Contains(a.AccountId.ToString())).Count();

                    meterReadings.RemoveAll(a => !validAccountIds.Contains(a.AccountId.ToString()));

                    return meterReadings;
                }
            }

            return null;
        }
    }
}

