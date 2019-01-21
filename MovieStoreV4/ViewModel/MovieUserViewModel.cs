using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStoreV4.Models;

namespace MovieStoreV4.ViewModel
{
    public class MovieUserViewModel
    {
        public  tbl_Movie modelMovie { get; set; }
        public List<tbl_Movie> modelMovies { get; set; }
        public tbl_MovieUser modelMovieUser { get; set; }
        public List<tbl_MovieUser> modelMovieUsers { get; set; }
        public List<tbl_Movie> modelBouthMovies { get; set; }
        

    }
}