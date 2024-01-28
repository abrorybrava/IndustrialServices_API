namespace IndustrialServices_API.Models
{
    public class ProdukModel
    {
        public int id_produk { get; set; }
        public string nama_produk { get; set; }
        public int id_tipe_produk { get; set; }
        public string pesanan { get; set; }
        public string deskripsi_produk { get; set; }
        public int status { get; set; }

        public string path_foto_produk { get; set; }
    }
}
