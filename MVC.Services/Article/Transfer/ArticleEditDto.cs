namespace MVC.Services.Article.Transfer
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ArticleEditDto
	{
        public ArticleEditDto()
        {
            this.LanguageId = 1;
        }

        public int ArticleId { get; set; }

        public int ArticleVersionId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
		public string Title { get; set; }
        
        [Required, AllowHtml]
		public string Content { get; set; }

        public bool Publish { get; set; }
	}
}
