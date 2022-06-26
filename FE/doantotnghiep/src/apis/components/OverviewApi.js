import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class OverviewAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/overview";
    } 
    getTurnoverTotal(month){
      let urlFull = this.controller + "/turnover-total";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }
    getOrderTotal(month){
      let urlFull = this.controller + "/order-total";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }
    getReceiptTotal(month){
      let urlFull = this.controller + "/receipt-total";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }
    getNewProductTotal(month){
      let urlFull = this.controller + "/new-product-total";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }
    getMonthlyTurnover(){
      let urlFull = this.controller + "/monthly-turnover";
      return BaseAPIConfig.get(urlFull);
    }
    getQuantitySoldByManufacturer(month){
      let urlFull = this.controller + "/quantity-sold-by-manufacturer";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }
    getQuantityOrderCancelled(month){
      let urlFull = this.controller + "/quantity-order-cancelled";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }
    getProductBestSellingsInMonth(param){
      let urlFull = this.controller + "/best-selling";
      return BaseAPIConfig.get(urlFull,{params:param});
    }
    getCancelledOrders(month){
      let urlFull = this.controller + "/cancelled-order";
      return BaseAPIConfig.get(urlFull,{params:{month:month}});
    }

  }
  
export default new OverviewAPI();