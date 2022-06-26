import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class ReceiptAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/receipts";
    } 
     insertReceipt(param){
      let urlFull = this.controller + "/insert";
      return BaseAPIConfig.post(urlFull,param);
     }
     updateReceipt(id,body){
      let urlFull = this.controller + `/update?id=${id}`;
      return BaseAPIConfig.put(urlFull,body);
     }
     getPaging(param){
      let urlFull = this.controller + "/receipt-paging";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     getReceiptDetailbyReceiptID(param){
      let urlFull = "/v1/receiptdetails/receipt-detail";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     getSuplierReceipt(param){
      let urlFull = this.controller + "/suplier-receipt";
      return BaseAPIConfig.get(urlFull,{params:param});
     }
     getRecetptByID(id){
       let urlFull=this.controller + `/id/${id}`
       return BaseAPIConfig.get(urlFull);
     }
}
export default new ReceiptAPI();