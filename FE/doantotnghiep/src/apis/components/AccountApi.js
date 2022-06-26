import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class AccountAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/account";
    } 
    checkAccountExists(params){
      let urlFull = this.controller + "/login";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    getAccountID(params){
      let urlFull = this.controller + "/accountID";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    getAccountByUserName(params){
      let urlFull = this.controller + "/account";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    checkAccountExistsInDB(params){
      let urlFull=this.controller+"/check-exists";
      return BaseAPIConfig.get(urlFull,{params:params})
    }
  }
  
export default new AccountAPI();