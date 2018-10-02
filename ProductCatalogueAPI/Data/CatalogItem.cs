namespace ProductCatalogueAPI.Data
{
    public class CatalogItem
    {
        public object Price { get; internal set; }
        public object Id { get; internal set; }
        public object Name { get; internal set; }
        public object Description { get; internal set; }
        public object Picture { get; internal set; }
        public object CatalogBrand { get; internal set; }
        public object CatalogBrandId { get; internal set; }
        public object CatalogType { get; internal set; }
        public object PictureURL { get; internal set; }
        public object CatalogTypeId { get; internal set; }
    }
}