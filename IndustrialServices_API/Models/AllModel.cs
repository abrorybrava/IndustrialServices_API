namespace IndustrialServices_API.Models
{
    public class AllModel
    {
        /*Artikel*/
        public int id_artikel { get; set; }
        public int id_pengelola { get; set; }
        public DateTime tanggal_rilis { get; set; }
        public string judul_artikel { get; set; }
        public string isi_artikel { get; set; }
        public string sampul_artikel { get; set; }

        /*Artikel*/

        /*Faculty Member*/
        public int id_pengajar { get; set; }
        public string npk { get; set; }
        public string nama_pengajar { get; set; }
        public string bidang_keahlian { get; set; }
        public string foto_pengajar { get; set; }
        public string deskripsi_pengajar { get; set; }
        /*Faculty Member*/

        /*Fasilitas*/
        public int id_fasilitas { get; set; }
        public string nama_fasilitas { get; set; }
        public int id_tipe_fasilitas { get; set; }
        public string deskripsi_fasilitas { get; set; }
        /*Fasilitas*/

        /*Foto Fasilitas*/
        public int id_foto_fasilitas { get; set; }
        public string path_foto_fasilitas { get; set; }
        /*Foto Fasilitas*/

        /*Pelatihan*/
        public int id_pelatihan { get; set; }
        public string nama_pelatihan { get; set; }
        public DateTime tanggal_pelatihan_awal { get; set; }
        public DateTime tanggal_pelatihan_akhir { get; set; }
        public int id_tipe_pelatihan { get; set; }
        public string deskripsi_pelatihan { get; set; }
        /*Pelatihan*/

        /*Foto Pelatihan*/
        public int id_foto_pelatihan { get; set; }
        public string path_foto_pelatihan { get; set; }
        /*Foto Pelatihan*/

        /*Produk*/
        public int id_produk { get; set; }
        public string nama_produk { get; set; }
        public int id_tipe_produk { get; set; }
        public string pesanan { get; set; }
        public string deskripsi_produk { get; set; }
        /*Produk*/

        /*Foto Produk*/
        public int id_foto_produk { get; set; }
        public string path_foto_produk { get; set; }
        /*Foto Produk*/


        public int status { get; set; }

    }
}
