using System;
namespace Blog.Web.Repository
{
	public interface IImageRepository
	{
		Task<string?> UploadAsync(IFormFile file);
	}
}

