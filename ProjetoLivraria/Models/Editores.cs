using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    public class Editores
    {
        public decimal edi_id_editor { get; set; }
        public string edi_nm_editor { get; set; }
        public string edi_ds_email { get; set; }
        public string edi_ds_url { get; set; }

        public Editores(decimal edi_id_editor, string edi_nm_editor, string edi_ds_email, string edi_ds_url)
        {
            this.edi_id_editor = edi_id_editor;
            this.edi_nm_editor = edi_nm_editor;
            this.edi_ds_email = edi_ds_email;
            this.edi_ds_url = edi_ds_url;
        }
    }
}