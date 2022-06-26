import BaseAPI from "../base/BaseApi";
class ManufacturerAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/manufacturer";
    } 
  }
  
export default new ManufacturerAPI();