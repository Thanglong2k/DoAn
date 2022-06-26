import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class PostAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/posts";
    }
    /**
     * danh sách bình luận theo sản phẩm
     * TTLONG 19/07/2021
     */
     getPostByProductID(param) {
      let urlFull = this.controller + "/posts-by-product";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    getPostDetailByID(param) {
      let urlFull = this.controller + "/post-by-id";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    /**
     * danh sách bình luận
     * TTLONG 19/07/2021
     */
     getPosts(param) {
      let urlFull = this.controller + "/posts";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    insertPostOfProduct(body){
      let urlFull = this.controller + "/create";
      return BaseAPIConfig.post(urlFull, body);
    }
    updatePostOfProduct(id,body){
      let urlFull = this.controller + `/update?id=${id}`;
      return BaseAPIConfig.put(urlFull, body);
    }
  }
  
export default new PostAPI();