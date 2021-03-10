using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Ajax.Utilities;
using MySocialNetwork.Models.SocialNetwork;

namespace MySocialNetwork.DAO
{
    public class ContentManager
    {
        private SocialNetworkDbContext dbContext = new SocialNetworkDbContext();

        public ContentType GetContentType(ContentTypes type)
        {
            ContentType contentType = dbContext.ContentTypes.Where(ct => ct.Title == type.ToString().ToLower())
                .First();
            return contentType;
        }

        public Content AddOneItem(Content content)
        {
            Content addedContent = dbContext.Content.Add(content);
            dbContext.SaveChanges();
            return addedContent;
        }
        
        public List<Content> AddContent(List<Content> content)
        {
            List<Content> addedContent = new List<Content>();
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (Content con in content)
                    {
                        Content newContent = dbContext.Content.Add(con);
                        addedContent.Add(newContent);
                    }
                    dbContext.SaveChanges();
                   transaction.Commit();
                    return addedContent;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}