﻿using Models.DTOs.Subforums.Output;
using Models.DTOs.Users.Output;

namespace Models.DTOs.Posts.Output
{
    public class PostListDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int VoteTally { get; set; }
        public int CommentCount { get; set; }
        public UserMinInfoDto User { get; set; }
        public SubforumMinInfoDto Subforum { get; set; }
    }
}
