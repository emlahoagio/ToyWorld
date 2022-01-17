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

        public CrawlDataJapanFigureServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private ICollection<Image> getImages(HtmlDocument toyDetail)
        {
            var result = new List<Image>();
            var imageDoc = toyDetail.DocumentNode.SelectNodes("//div[@id='sliderproduct']//img");
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
            return brand;
        }

        public async Task<IEnumerable<Toy>> getToy(string crawlLink)
        {
            var result = new List<Toy>();
            //open xml document
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            //open xml list toy
            HtmlAgilityPack.HtmlDocument doc = web.Load(crawlLink);
            Console.WriteLine("----------------------");
            Console.WriteLine("Is document null");
            Console.WriteLine(doc);
            Console.WriteLine("----------------------");
            HtmlNodeCollection nodeList = doc.DocumentNode.SelectNodes("//div[@class='product-information']");
            //foreach (var toyNode in nodeList)
            for (int i=0; i < nodeList.Count; i++)
            {
                var toyNode = nodeList.ElementAt(i);
                var name = "";
                var description = "";
                var price = "";
                var coverImage = "";
                var brandName = "";

                coverImage = toyNode.SelectNodes("//img[@class='image-hover']").ElementAt(i).Attributes["src"].Value;
                price = toyNode.SelectNodes("//span[@class='price price-new flexbox-content text-left']").ElementAt(i).InnerText.Trim();
                var detailLink = toyNode.SelectNodes("//h2[@class='product-title name']//a").ElementAt(i).Attributes["href"].Value;

                HtmlAgilityPack.HtmlDocument toyDetail = web.Load(FigureDomain + detailLink.ToString());
                var detailDoc = toyDetail.DocumentNode.SelectNodes("//span[@style='color:#333333']");
                foreach (var detailNode in detailDoc)
                {
                    var innverText = detailNode.InnerText;
                    if (!innverText.Contains(":"))
                    {
                        name = innverText;
                    }
                    if (innverText.Contains("Hãng sản xuất"))
                    {
                        brandName = innverText.Substring(detailNode.InnerText.IndexOf(":") + 1).Trim();
                    }
                    else
                    {
                        description += detailNode.InnerText + "\n";
                    }
                }

                //get image
                var imageList = getImages(toyDetail);

                //check brand
                var brand = await checkBrand(brandName);
                var toy = new Toy
                {
                    Name = name,
                    Description = description,
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
