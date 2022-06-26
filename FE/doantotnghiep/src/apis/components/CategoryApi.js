import BaseAPI from "../base/BaseApi";
class CategoryAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/category";
    } 
  }
  
export default new CategoryAPI();