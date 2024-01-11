namespace IndustrialServices_API.Models
{
    public class PelatihanModel
    {
        public int id_pelatihan { get; set; }
        public string nama_pelatihan { get; set; }
        public int id_pengajar { get; set; }
        public DateTime tanggal_pelatihan_awal { get; set; }
        public DateTime tanggal_pelatihan_akhir { get; set; }
        public string jenis_pelatihan { get; set; }
        public string deskripsi_pelatihan { get; set; }
        public int status { get; set; }

        public int id_fasilitas { get; set; }
    }
}
