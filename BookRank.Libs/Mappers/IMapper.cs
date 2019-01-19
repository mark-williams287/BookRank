﻿using Amazon.DynamoDBv2.Model;
using BookRank.Contracts;
using System.Collections.Generic;

namespace BookRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<BookResponse> ToBookContract(ScanResponse response);

        IEnumerable<BookResponse> ToBookContract(QueryResponse response);

        BookResponse ToBookContract(GetItemResponse response);
    }
}
