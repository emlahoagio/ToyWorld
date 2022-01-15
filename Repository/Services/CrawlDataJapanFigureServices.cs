using Contracts;
using Contracts.Services;
using Entities.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class CrawlDataJapanFigureServices : ICrawlDataJapanFigureServices
    {

        private readonly string FigureDomain = "https://japanfigure.vn";
        private readonly IRepositoryManager _repositoryManager;
        private string[] removeChar = {"\t", "&nbsp;"};

        public CrawlDataJapanFigureServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private ICollection<Image> getImages(HtmlDocument toyDetail)
        {
            var result = new List<Image>();
            var imageDoc = toyDetail.DocumentNode.SelectNodes("//div[@id='sliderproduct']//img");
            if (imageDoc == null) return null;
            foreach (var img in imageDoc)
            {
                string imgLink = img.Attributes["src"].Value;
                Image image = new Image { Url = "https:" + imgLink};
                result.Add(image);
            }
            return result;
        }

        private async Task<Brand> checkBrand(string brandName)
        {
            var brand = await _repositoryManager.Brand.GetBrandByName(brandName, trackChanges: false);
            if (brand == null)
            {
                //create brand
                brand = new Brand { Name = brandName };
                _repositoryManager.Brand.CreateBrand(brand);
                await _repositoryManager.SaveAsync();
                brand = await _repositoryManager.Brand.GetBrandByName(brandName, trackChanges: false);
            }
            if(brandName == "")
            {
                brand = await _repositoryManager.Brand.GetBrandByName("Unknow Brand", trackChanges: false);
            }
            return brand;
        }

        public async Task<IEnumerable<Toy>> getToy(string crawlLink)
        {
            var result = new List<Toy>();
            //open xml document
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            //open xml list toy
            HtmlAgilityPack.HtmlDocument doc = web.Load(crawlLink);
            Console.WriteLine("Crawling data from " + crawlLink);
            Console.WriteLine("================================");
            Console.WriteLine(doc.DocumentNode);
            Console.WriteLine("================================");
            var nodeList = doc.DocumentNode.SelectNodes("//div[@class='product-detail clearfix']");
            //foreach (var toyNode in nodeList)
            for(int i=0; i < nodeList.Count; i++)
            {
                var toyNode = nodeList.ElementAt(i);
                var name = "";
                var description = "";
                var price = "";
                var coverImage = "";
                var brandName = "";

                //Get cover image
                var imageNodeCount = toyNode.SelectNodes("//img[@class='image-hover']").Count;
                if (imageNodeCount == nodeList.Count)
                {
                    coverImage = toyNode.SelectNodes("//img[@class='image-hover']").ElementAt(i).Attributes["src"].Value;
                }else
                {
                    if(i == 0)
                    {
                        coverImage = toyNode.SelectNodes("//div[@class='product-detail clearfix']//div[@class='product-image image-resize']//img")
                        .FirstOrDefault().Attributes["src"].Value;
                    }else
                    {
                        coverImage = toyNode.SelectNodes("//img[@class='image-hover']").ElementAt(i - 1).Attributes["src"].Value;
                    }
                }

                //get price
                price = toyNode.SelectNodes("//span[@class='price price-new flexbox-content text-left']").ElementAt(i).InnerText.Trim();
                
                //read detail
                var detailLink = toyNode.SelectNodes("//h2[@class='product-title name']//a").ElementAt(i).Attributes["href"].Value;

                HtmlAgilityPack.HtmlDocument toyDetail = web.Load(FigureDomain + detailLink.ToString());
                var detailDoc = toyDetail.DocumentNode.SelectNodes("//div[@id='description2']//p");
                name = toyDetail.DocumentNode.SelectNodes("//div[@class='product-title']//h1").FirstOrDefault().InnerText.Trim();
                if (detailDoc != null)
                {
                    foreach (var detailNode in detailDoc)
                    {
                        var innerText = detailNode.InnerText;
                        if (innerText.Contains("Hãng sản xuất"))
                        {
                            brandName = innerText.Substring(detailNode.InnerText.IndexOf(":") + 1).Trim();
                        }
                        else
                        {
                            description += innerText + "\n";
                        }
                    }
                }

                //get image
                var imageList = getImages(toyDetail);

                //check brand
                var brand = await checkBrand(brandName);
                var editedDescription = description.Replace("&nbsp;"," ").Replace("\t","");
                var toy = new Toy
                {
                    Name = name,
                    Description = editedDescription,
                    Price = decimal.Parse(price.Substring(0, price.Length - 1)),
                    CoverImage = "https:"+coverImage,
                    BrandId = brand.Id,
                    Images = imageList
                };
                result.Add(toy);
            }
            return result;
        }
    }
}
