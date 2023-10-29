using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace u22565991_HW03.Models
{
    public class CombinedViewModel
    {
        public IPagedList<students> Students { get; set; }
        public IPagedList<books> Books { get; set; }


        public IPagedList<borrows> Borrows { get; set; }

        public IPagedList<authors> Authors { get; set; }


        public IPagedList<types> Types { get; set; }

        public List<StudentBorrowingData> StudentBorrowingData { get; set; }

        public List<DocumentModel> DocumentModel { get; set; }
    }
}