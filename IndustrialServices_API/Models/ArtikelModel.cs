namespace IndustrialServices_API.Models
{
    public class ArtikelModel
    {
        public int id_artikel { get;set; }
        public int id_pengelola { get;set; }
        public DateTime tanggal_rilis { get;set; }
        public string judul_artikel { get; set; }
        public string isi_artikel { get; set; }
        public string sampul_artikel { get; set; }
        public int status { get; set; }
    }
}
