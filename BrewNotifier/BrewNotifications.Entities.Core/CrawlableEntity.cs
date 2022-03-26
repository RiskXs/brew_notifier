using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewNotifications.Entities.Core
{
    public class CrawlableEntity: IDeleteable
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public CrawlingStatus CrawlingStatus { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlacklisted { get; set; }
        public DateTime? LastCrawled { get; set; }
    }

    public enum CrawlingStatus
    {
        NOT_CRAWLED = 0,
        ERROR_REQUESTING_LINK = 1,
        UPDATING_LINK = 2,
        MARKED_AS_DUPLICATE = 3,
        UPDATED_LINK = 4,
        CRAWLING = 5,
        CRAWLING_FAILED = 6,
        RESCHEDULED_LONG_CRAWLING = 7,
        CRAWLING_TOO_LONG = 8,
        HAS_NO_PAGES = 9,
        TEXT_UPLOADED = 10,
        AWAITING_CRAWL = 11,
        INDEXED_BY_ELASTIC = 12,
        TEXT_ANALYZED = 13,
        DOMAIN_INVALID = 14,
        NO_LINKS_IN_PAGE = 15,
        UNCRAWLABLE = 16
    }
}
