using System;
using System.Collections.Generic;
using System.Linq;

namespace MvaCToolkitCs.v1_2.Net.HttpWebTx
{
    public class CtkNetUserAgents
    {

        static List<CtkNetUserAgent> m_chromeUserAgents;

        public static List<CtkNetUserAgent> ChromeUserAgents
        {
            get
            {
                if (m_chromeUserAgents == null)
                {
                    m_chromeUserAgents = new List<CtkNetUserAgent>();


                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "78", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "77", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "76", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "74", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "72", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "69", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "68", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "67", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "65", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "63", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "63", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "60", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "46", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "21", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.83 Safari/537.1" });
                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Linux", HardwareType = "Computer", Version = "44", UpdateTime = new DateTime(2021, 01, 01), UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36" });

                    m_chromeUserAgents.Add(new CtkNetUserAgent() { OS = "Windows", HardwareType = "Computer", Version = "??", UpdateTime = new DateTime(2022, 03, 12), UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36" });

                }
                return m_chromeUserAgents;
            }
        }

        public static CtkNetUserAgent Random() { return Random(new DateTime(0)); }

        public static CtkNetUserAgent Random(DateTime dt)
        {
            var rnd1 = new Random((int)DateTime.Now.Ticks);
            var rnd2 = new Random((int)DateTime.Now.Ticks);

            var list = ChromeUserAgents.Where(x => x.UpdateTime >= dt).ToList();
            var index = 0;

            var cnt = rnd2.Next(10);//要取Random陣列的第幾個;
            for (int idx = 0; idx < cnt; idx++)
                index = rnd1.Next(list.Count);

            return list[index];
        }


    }
}
