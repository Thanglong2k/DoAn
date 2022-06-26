import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class CommentAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/comments";
    }
    /**
     * danh sách bình luận theo sản phẩm
     * TTLONG 19/07/2021
     */
     getCommentByProductID(param) {
      let urlFull = this.controller + "/comments-by-product";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    /**
     * danh sách bình luận
     * TTLONG 19/07/2021
     */
     getComments(param) {
      let urlFull = this.controller + "/comments";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    insertComment(body) {
      let urlFull = this.controller + "/add-comment";
      return BaseAPIConfig.post(urlFull,body);
    }
  }
  
export default new CommentAPI();