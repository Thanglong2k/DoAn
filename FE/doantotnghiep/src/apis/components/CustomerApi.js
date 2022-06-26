import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class CustomerAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/customers";
    }
    /**
     * danh sách bình luận theo sản phẩm
     * TTLONG 19/07/2021
     */
     getCustomerByPhoneNumber(param) {
      let urlFull = this.controller + "/customer-by-phonenumber";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
  }
  
export default new CustomerAPI();