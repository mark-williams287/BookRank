﻿using BookRank.Contracts;
using BookRank.Libs.Mappers;
using BookRank.Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRank.Services
{
    public class BookRankService : IBookRankService
    {
        private readonly IBookRankRepository _bookRankRepository;

        private readonly IMapper _mapper;

        public BookRankService(IBookRankRepository bookRankRepository, IMapper mapper)
        {
            _bookRankRepository = bookRankRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            var response = await _bookRankRepository.GetAllBooks();

            return _mapper.ToBookContract(response);
        }

        public async Task<BookResponse> GetBook(int userId, string bookName)
        {
            var response = await _bookRankRepository.GetBook(userId, bookName);

            return _mapper.ToBookContract(response);
        }

        public async Task<IEnumerable<BookResponse>> GetUsersRankedBooksByTitle(int userId, string bookName)
        {
            var response = await _bookRankRepository.GetUsersRankedBooksByTitle(userId, bookName);

            return _mapper.ToBookContract(response);
        }

        public async Task AddBook(int userId, BookRankRequest request)
        {
            await _bookRankRepository.AddBook(userId, request);
        }

        public async Task UpdateBook(int userId, BookUpdateRequest request)
        {
            await _bookRankRepository.UpdateBook(userId, request);
        }

        public async Task<BookRankResponse> GetBookRank(string bookName)
        {
            var response = await _bookRankRepository.GetBookRank(bookName);

            var overallBookRanking = Math.Round(response.Items.Select(x => Convert.ToInt32(x["Ranking"].N)).Average());

            return new BookRankResponse
            {
                BookName = bookName,
                OverallRanking = overallBookRanking
            };
        }
    }
}
