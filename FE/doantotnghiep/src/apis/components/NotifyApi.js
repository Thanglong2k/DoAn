import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class NotifyAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/notify";
    } 
    getNotifies(){
      let urlFull = this.controller + "/notify";
      return BaseAPIConfig.get(urlFull);
    }
    getNotifyNotViewTotal(){
      let urlFull = this.controller + "/not-view-notify-total";
      return BaseAPIConfig.get(urlFull);
    }
    updateIsView(){
      let urlFull = this.controller + "/update-view";
      return BaseAPIConfig.put(urlFull);
    }
    updateIsRead(params){
      let urlFull = this.controller + "/update-read";
      return BaseAPIConfig.put(urlFull,null,{params:params});
    }
  }
  
export default new NotifyAPI();