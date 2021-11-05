using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WJS_BlogsNPosts.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public IList<Post> Posts { get; set; }
    }
}
