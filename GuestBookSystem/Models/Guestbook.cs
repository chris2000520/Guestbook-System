using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GuestBookSystem.Models
{
    public class Guestbook
    {
        public int GuestbookId { get; set; }

        [Required(ErrorMessage = "留言标题不能为空")]
        [MaxLength(20, ErrorMessage = "留言标题不超过20个字符")]
        public string Title { get; set; } //留言标题



        [Required(ErrorMessage = "留言内容不能为空")]
        [MinLength(10, ErrorMessage = "留言内容不少于10个字符")]
        public string Content { get; set; } //留言内容



        [Required(ErrorMessage = "留言人的邮箱不能为空")]
        [EmailAddress(ErrorMessage = "email格式不对")]
        public string AuthorEmail { get; set; } //留言邮箱



        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; } //创建日期时间

        public Boolean IsPass { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}