﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstact
{
    public interface IMovieDal:IGenericDal<Movie>
    {
        List<Movie> GetMoviesByListID(int id);



    }
}
