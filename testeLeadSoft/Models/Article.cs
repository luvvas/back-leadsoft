﻿namespace testeLeadSoft.Models
{
	public class Article
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Text { get; set; }
		public Author Author { get; set; }
		public Guid AuthorId { get; set; }
		public Category Category { get; set; }
		public Guid CategoryId { get; set; }
		public List<Comment> Comments { get; set; }
	}
}
