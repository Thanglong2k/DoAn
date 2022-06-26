using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Service
{
    public interface IProductHub
    {
        Task NewCommentAddedToProduct(List<CustomerComment> comment);
        Task NewCommentAddedToAdmin(List<CommentProductCustomer> comment);
    }
}
