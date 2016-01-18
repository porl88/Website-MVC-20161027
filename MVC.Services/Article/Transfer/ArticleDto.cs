namespace MVC.Services.Article.Transfer
{
	using System;

	public class ArticleDto
	{
		public int Id { get; set; }

		public string Title { get; set; }

        public string Author { get; set; }

		public string Content { get; set; }

        public DateTimeOffset? Published { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
	}
}
