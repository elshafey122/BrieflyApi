
namespace Briefly.Infrastructure.SeedData
{
    public class SeedRss
    {
        public static async Task SeedRssAsync(IRssRepository _rssRepository)
        {
            if (_rssRepository.GetTableNoTracking().Count() == 0)
            {
                List<RSS> Rsses = new()
                {
                    new RSS()
                    {
                        Title="BBC News",
                        Description="BBC News - News Front Page",
                        Link="http://newsrss.bbc.co.uk/rss/newsonline_world_edition/front_page/rss.xml",
                        Image="https://news.bbcimg.co.uk/nol/shared/img/bbc_news_120x60.gif",
                    },
                    new RSS()
                    {
                        Title="ElWatanNews.com Rss feed",
                        Description="ElWatanNews.com Rss feed",
                        Link="http://www.elwatannews.com/home/rssfeeds",
                        Image="https://www.elwatannews.com/content/gfx/logo.png",
                    },
                    new RSS()
                    {
                        Title="world sport",
                        Description="newest sport world news",
                        Link="https://www.youm7.com/rss/SectionRss?SectionID=332",
                        Image="https://www.youm7.com/images/centerlogo.png?2",
                    },
                    new RSS()
                    {
                        Title="احدث الموضوعات",
                        Description="احدث الموضوعات احدث الموضوعات والأخبار من البوابة نيوز albawabhnews.com",
                        Link="https://albawabhnews.com/rss.aspx",
                        Image="https://www.albawabhnews.com/Upload/files/0/4/19.png",
                    },
                    new RSS()
                    {
                        Title="CNN.com - RSS Channel",
                        Description="CNN.com delivers up-to-the-minute news and information on the latest top stories, weather, entertainment, politics and more.",
                        Link="http://rss.cnn.com/rss/cnn_latest.rss",
                        Image="http://i2.cdn.turner.com/cnn/2015/images/09/24/cnn.digital.png",
                    },
                    new RSS()
                    {
                        Title="World news | The Guardian",
                        Description="Latest World news news, comment and analysis from the Guardian, the world's leading liberal voice",
                        Link="http://feeds.guardian.co.uk/theguardian/world/rss",
                        Image="https://assets.guim.co.uk/images/guardian-logo-rss.c45beb1bafa34b347ac333af2e6fe23f.png",
                    },
                    new RSS()
                    {
                        Title="Technology | The Guardian",
                        Description="Latest Technology news, comment and analysis from the Guardian, the world's leading liberal voice",
                        Link="http://www.theguardian.com/technology/rss",
                        Image="https://assets.guim.co.uk/images/guardian-logo-rss.c45beb1bafa34b347ac333af2e6fe23f.png",
                    },
                    new RSS()
                    {
                        Title="Football | The Guardian",
                        Description="Football news, results, fixtures, blogs, podcasts and comment on the Premier League, European and World football from the Guardian, the world's leading liberal voice",
                        Link="http://www.theguardian.com/football/rss",
                        Image="https://assets.guim.co.uk/images/guardian-logo-rss.c45beb1bafa34b347ac333af2e6fe23f.png",
                    },
                    new RSS()
                    {
                        Title="World News | Latest Top Stories | Reuters",
                        Description="Reuters.com is your online source for the latest world news stories and current events, ensuring our readers up to date with any breaking news development",
                        Link="https://rsshub.app/reuters/world",
                        Image="https://www.reuters.com/pf/resources/images/reuters/logo-vertical-default-512x512.png?d=116",
                    },
                    new RSS()
                    {
                        Title="Latest Political News on Fox News",
                        Description="Read all about the political news happening with Fox News. Learn about political parties, political campaigns, and international politics today.",
                        Link="http://feeds.foxnews.com/foxnews/politics",
                        Image="https://global.fncstatic.com/static/orion/styles/img/fox-news/logos/fox-news-desktop.png",
                    },
                    new RSS()
                    {
                        Title="Technology News Articles on Fox News",
                        Description="Explore all the news happening in the technology industry with Fox News. Check out the latest tech launches and computer tech updates going on today.",
                        Link="http://feeds.foxnews.com/foxnews/scitech",
                        Image="https://global.fncstatic.com/static/orion/styles/img/fox-news/logos/fox-news-desktop.png",
                    },
                    new RSS()
                    {
                        Title="Coding Horror",
                        Description="programming and human factors",
                        Link="http://feeds.feedburner.com/codinghorror",
                        Image="https://blog.codinghorror.com/favicon.png",
                    },
                    new RSS()
                    {
                        Title="Sky News Arabia",
                        Description="موقع إخباري شامل تتابعون فيه مستجدات الأحداث العربية والعالمية على مدار الساعة، وتغطية مستمرة لأخبار السياسة والرياضة والاقتصاد والعلوم والفن والتكنولوجيا.",
                        Link="http://www.skynewsarabia.com/web/rss/middle-east.xml",
                        Image="https://images.skynewsarabia.com/images/v1/2013/10/08/458410/1200/630/1-458410.jpg",
                    },
                    new RSS()
                    {
                        Title="almasryalyoum",
                        Description=" مؤسسة إعلامية مصرية مستقلة انطلقت عام 2003، تعتمد فى تقديم خدماتها الإعلامية على مجموعة من أفضل الصحفيين المصريين، ويمتد نطاق التغطية الخبرية إلى جميع أنحاء مصر عبر شبكة متميزة من المراسلين",
                        Link="https://www.almasryalyoum.com/rss/rssfeeds",
                        Image="https://mediaaws.almasryalyoum.com/news/verylarge/2017/10/24/735167_0.JPG",
                    },
                    new RSS()
                    {
                        Title="abcnews",
                        Description="Your trusted source for breaking news, analysis, exclusive interviews, headlines",
                        Link="http://feeds.abcnews.com/abcnews/topstories",
                        Image="https://upload.wikimedia.org/wikipedia/commons/3/3e/ABC_News_splash.png",
                    },
                    new RSS()
                    {
                        Title="Egypt Independent",
                        Description="PM delivers speech in opening of second day of Egypt",
                        Link="http://www.egyptindependent.com/feed/",
                        Image="https://amayei.nyc3.digitaloceanspaces.com/2018/09/icon-1-32x32.png",
                    },
                    new RSS()
                    {
                        Title="محتوى جريدة الشروق RSS - مصر- بوابة الشروق",
                        Description="Explore all the news happening in the technology industry with Fox News. Check out the latest tech launches and computer tech updates going on today.",
                        Link="http://www.shorouknews.com/egypt/rss",
                        Image="https://www.shorouknews.com/App_Themes/images/shorouknewsLogo.png",
                    },
                    new RSS()
                    {
                        Title="NYT > Top Stories",
                        Description="The latest top stories from the New York Times.",
                        Link="http://www.nytimes.com/services/xml/rss/nyt/HomePage.xml",
                        Image="https://static01.nyt.com/images/misc/NYT_logo_rss_250x40.png",
                    },
                    new RSS()
                    {
                        Title="Washington Post",
                        Description="Washington Post News Feed",
                        Link="http://www.washingtonpost.com/wp-dyn/rss/politics/index.xml",
                        Image="https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Washington_Post_logo_circa_2016.svg/1024px-Washington_Post_logo_circa_2016.svg.png",
                    },
                    new RSS()
                    {
                        Title="اخبار الاردن | وكالة عمون الاخبارية",
                        Description="Latest news from Jordan",
                        Link="http://www.ammonnews.net/rss.php?type=news&id=50",
                        Image="https://www.ammonnews.net/images/AmmonNewsLogo.png",
                    },
                    new RSS()
                    {
                        Title="Daily News Egypt",
                        Description="Egypt’s Only Daily Independent Newspaper In English",
                        Link="http://feeds.feedburner.com/DailyNewsEgypt",
                        Image="https://d1b3667xvzs6rz.cloudfront.net/2023/03/83187629_10157628130731265_5149454784750682112_n-150x150.png",
                    },
                    new RSS()
                    {
                        Title="Masrawy-أخبار مصر",
                        Description="تغطية متميزة لكل الأخبار والأحداث في مصر، ووضعها أمام عين القارئ دون تحريف أو تزييف",
                        Link="http://www.masrawy.com/News/rss/LocalPolitics.aspx",
                        Image="https://www.masrawy.com/Version2015/Images/masrawyLogo.png",
                    },
                    new RSS()
                    {
                        Title="Egyptian Streets",
                        Description="Independent Media",
                        Link="http://egyptianstreets.com/feed/",
                        Image="https://egyptianstreets.com/wp-content/uploads/2015/09/cropped-eslogo-32x32.png",
                    },
                    new RSS()
                    {
                        Title="rss-اقتصاد وبورصة",
                        Description="يقدم الموقع خدمات إخبارية، وتغطيات، ومساحة للكتابة وعرض الآراء، ويقدم خدمة البريد الإلكتروني",
                        Link="https://www.youm7.com/rss/SectionRss?SectionID=297",
                        Image="http://https://www.youm7.com/images/centerlogo.png?2",
                    },
                    new RSS()
                    {
                        Title="rss-علوم و تكنولوجيا",
                        Description="يقدم الموقع خدمات إخبارية، وتغطيات، ومساحة للكتابة وعرض الآراء، ويقدم خدمة البريد الإلكتروني",
                        Link="https://www.youm7.com/rss/SectionRss?SectionID=328",
                        Image="http://https://www.youm7.com/images/centerlogo.png?2",
                    }
                };
                await _rssRepository.AddRangeAsync(Rsses);
            }
        }
    }
}

