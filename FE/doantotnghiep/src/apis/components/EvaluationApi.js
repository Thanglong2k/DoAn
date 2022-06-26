import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class EvaluationAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/evaluations";
    }
    /**
     * Lấy số lượng đánh giá của từng mức đánh giá
     * TTLONG 19/07/2021
     */
     getProductRatingDetail(param) {
      let urlFull = this.controller + "/product-rating-detail";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    /**
     * danh sách đánh giá theo sản phẩm
     * TTLONG 19/07/2021
     */
     getEvaluationByProductID(param) {
      let urlFull = this.controller + "/evaluations-by-product";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    getEvaluations(param){
      let urlFull = this.controller + "/evaluations";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
  }
  
export default new EvaluationAPI();