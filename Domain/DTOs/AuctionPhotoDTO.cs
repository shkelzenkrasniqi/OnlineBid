﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AuctionPhotoDTO
    {
        public byte[] PhotoData { get; set; }
        public string ContentType { get; set; }
    }
}
