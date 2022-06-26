import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class SuplierAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/supliers";
    }
    /**
     * danh sách bình luận theo sản phẩm
     * TTLONG 19/07/2021
     */
     getSuplierByPhoneNumber(param) {
      let urlFull = this.controller + "/suplier-by-phonenumber";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
  }
  
export default new SuplierAPI();