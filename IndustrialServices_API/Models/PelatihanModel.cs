namespace IndustrialServices_API.Models
{
    public class PelatihanModel
    {
        public int id_pelatihan { get; set; }
        public string nama_pelatihan { get; set; }
        public int id_pengajar { get; set; }
        public string nama_pengajar { get; set; }
        public DateTime tanggal_pelatihan_awal { get; set; }
        public DateTime tanggal_pelatihan_akhir { get; set; }
        public int id_tipe_pelatihan { get; set; }
        public string deskripsi_pelatihan { get; set; }
        public int status { get; set; }

        public int id_fasilitas { get; set; }
        public string path_foto_pelatihan { get; set; }
        public string bidang_keahlian { get; set; }
    }
}
