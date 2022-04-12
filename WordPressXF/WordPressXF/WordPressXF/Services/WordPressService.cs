using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressXF.Common;

namespace WordPressXF.Services
{
    internal class WordPressService
    {
        private readonly WordPressClient _client;

        public WordPressService()
        {
            _client = new WordPressClient(Statics.WordpressUrl);
        }

        public async Task<IEnumerable<Post>> GetLatestPostsAsync(int page = 0, int perPage = 20)
        {
            try
            {
                page++;

                var posts = await _client.Posts.QueryAsync(new PostsQueryBuilder
                {
                    Page = page,
                    PerPage = perPage,
                    Embed = true
                });

                return posts;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(WordPressService)} | {nameof(GetLatestPostsAsync)} | {ex}");
            }

            return null;
        }

        public async Task<List<CommentThreaded>> GetCommentsForPostAsync(int postid)
        {
            var comments = await _client.Comments.QueryAsync(new CommentsQueryBuilder
            {
                Posts = new List<int> { postid },
                Page = 1,
                PerPage = 100
            });

            return ThreadedCommentsHelper.GetThreadedComments(comments);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            await _client.Auth.RequestJWTokenAsync(username, password);

            var isAuthenticated = await _client.Auth.IsValidJWTokenAsync();

            if (isAuthenticated)
                return await _client.Users.GetCurrentUser();

            return null;
        }

        public void Logout()
        {
            _client.Auth.Logout();
        }

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            return await _client.Auth.IsValidJWTokenAsync();
        }

        public async Task<Comment> PostCommentAsync(int postId, string text, int replyTo = 0)
        {
            var comment = new Comment(postId, text);
            if (replyTo != 0)
                comment.ParentId = replyTo;

            return await _client.Comments.CreateAsync(comment);
        }
    }
}
