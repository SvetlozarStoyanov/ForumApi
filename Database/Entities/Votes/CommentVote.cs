﻿using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Enums.Votes;

namespace Database.Entities.Votes
{
    public class CommentVote
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long PostId { get; set; }
        public Comment Comment { get; set; }
        public PostVotes Type { get; set; }
    }
}
