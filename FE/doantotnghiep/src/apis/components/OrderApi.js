import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class OrderAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/orders";
    } 
    /**
     * Check khách hàng đã mua sản phẩm chưa
     */
     checkCustomerPurchasedProduct(param){
        let urlFull = this.controller + "/check-purchased-product";
        return BaseAPIConfig.get(urlFull,{params:param});
     }
     insertOrder(param){
      let urlFull = this.controller + "/insert";
      return BaseAPIConfig.post(urlFull,param);
     }
     updateOrder(id,body){
      let urlFull = this.controller + `/update?id=${id}`;
      return BaseAPIConfig.put(urlFull,body);
     }
     getPurchasedHistory(param){
      let urlFull = this.controller + "/purchased-history";
      return BaseAPIConfig.get(urlFull,{params:{phoneNumber:param}});
     }
     getFollowOrder(param){
      let urlFull = this.controller + "/follow-order";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     getPaging(param){
      let urlFull = this.controller + "/order-paging";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     getOrderDetailbyOrderID(param){
      let urlFull ="/v1/orderdetails/order-detail";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     getCustomerOrder(param){
      let urlFull = this.controller + "/customer-order";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     updateIMEIByOrderDetailID(params){
      let urlFull = this.controller + `/update-imei?orderDetailID=${params.orderDetailID}`;
      return BaseAPIConfig.put(urlFull,params.imeis);
     }
}
export default new OrderAPI();