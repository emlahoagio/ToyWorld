using Contracts;
using Contracts.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Repository.Services
{
    public class CrawlDataMyKingdomService : ICrawlDataMyKingdomService
    {
        private readonly IRepositoryManager _repositoryManager;
        public CrawlDataMyKingdomService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public List<String> GetListLink(string url)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            List<String> result = new List<string>();
            foreach (var item in doc.DocumentNode.SelectNodes("//a[@class='product-item-link']"))
            {
                string link = item.GetAttributeValue("href", "empty").Trim();
                if (!link.Equals("empty"))
                {
                    result.Add(item.GetAttributeValue("href", "empty"));
                }
            }
            return result;
        }
        public async Task<Toy> GetToyDetail(string url)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            Toy dto = new Toy();
            //Get name
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("//h1[@class='product-name']");
            dto.Name = node.InnerText.Trim();
            //Get brand
            node = doc.DocumentNode.SelectSingleNode("//div[@class='product-brand']/span/a");
            var brand = await checkBrand(node.InnerText.Trim());
            dto.BrandId = brand.Id;
            //Get pricech
            node = doc.DocumentNode.SelectSingleNode("//span[@class='price']");
            dto.Price = Decimal.Parse(node.InnerText.Trim().Substring(0, node.InnerText.Trim().Length - 4).Replace(",", ""));
            ////Get SKU (Mã VT)
            //node = doc.DocumentNode.SelectSingleNode("//div[@class='product attribute sku']/span[@class='value']");
            //dto.SKU = node.InnerText.Trim();
            //Get Description
            node = doc.DocumentNode.SelectSingleNode("//div[@class='product attribute description']/div[@class='value']");
            dto.Description = node.InnerText.Trim();
            //Get List Image
            dto.Images = new List<Image>();
            foreach (var item in doc.DocumentNode.SelectNodes("//a[@class='lb']/img[@class='img-responsive']"))
            {
                string link = item.GetAttributeValue("src", "empty").Trim();
                if (!link.Equals("empty"))
                {
                    dto.Images.Add(new Image
                    {
                        Url = link
                    });
                }
            }
            //Get cover Image
            dto.CoverImage = dto.Images.First().Url;
            return dto;
        }

        private async Task<Brand> checkBrand(string brandName)
        {
            var brand = await _repositoryManager.Brand.GetBrandByName(brandName, trackChanges: false);
            if (brand.Id == 5)
            {
                //create brand
                brand = new Brand { Name = brandName };
                _repositoryManager.Brand.CreateBrand(brand);
                await _repositoryManager.SaveAsync();
                brand = await _repositoryManager.Brand.GetBrandByName(brandName, trackChanges: false);
            }
            return brand;
        }
    }
}
