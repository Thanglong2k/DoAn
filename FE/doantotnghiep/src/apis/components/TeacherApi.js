import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class TeacherAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/teachers";
    } 
    /**
     * Get new employee code
     * TTLONG 19/07/2021
     */
    getNewTeacherCode() {
      let urlFull = this.controller + "/NewCode";
      return BaseAPIConfig.get(urlFull);
    }
    /**
     * Filter employee
     * TTLONG 20/07/2021
     */
    filter(params) {
      let urlFull =
        this.controller +
        `/teacherFilter`;
        // if(departmentId!=""){
        //   urlFull +=`&departmentId=${departmentId}`;
        // }
        // if(positionId!=""){
        //   urlFull +=`&positionId=${positionId}`;
        // }
        
      return BaseAPIConfig.get(urlFull,{params:params});
    }

    checkExistsCombinationSubject(id){
      let urlFull=this.controller+`/existsCombinationSubject`;
      return BaseAPIConfig.get(urlFull,{params:{combinationSubjectId:id}});
    }
    
  }
  
  export default new TeacherAPI();