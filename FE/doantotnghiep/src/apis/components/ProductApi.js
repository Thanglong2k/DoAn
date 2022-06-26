import BaseAPI from "../base/BaseApi";
import BaseAPIConfig from "../base/BaseAPIConfig";
class ProductAPI extends BaseAPI {
    constructor() {
      super();
      this.controller = "/v1/products";
    } 
    createProduct(body){
      let urlFull = this.controller + "/create";
      return BaseAPIConfig.post(urlFull,body);
    }
    updateProduct(id,body){
      let urlFull = this.controller + `/update?id=${id}`;
      return BaseAPIConfig.put(urlFull,body);
    }
    /**
     * Get new employee code
     * TTLONG 19/07/2021
     */
    getNewProductCode() {
      let urlFull = this.controller + "/NewCode";
      return BaseAPIConfig.get(urlFull);
    }
    /**
     * Get new product List
     * TTLONG 21/02/2022
     */
    getNewProducts(params) {
      let urlFull = this.controller + "/new-product";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    getNewProductPosts(params) {
      let urlFull = this.controller + "/new-product-post";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    /**
     * Get product List paging
     * TTLONG 21/02/2022
     */
    getProductsPaging(params) {
      let urlFull = this.controller + "/product-paging";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    /**
     * Get product and productAttribute List paging
     * TTLONG 21/02/2022
     */
    getProductAndProductAttribute(params) {
      let urlFull = this.controller + "/product-attribute";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    /**
     * Get best selling product List
     * TTLONG 21/02/2022
     */
     getBestSellingProducts(params) {
      let urlFull = this.controller + "/best-selling-product";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    /**
     * Get related product List
     * TTLONG 04/03/2022
     */
     getRelatedProducts(params) {
      let urlFull = this.controller + "/related-product";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    /**
     * Get recommand product List
     * TTLONG 20/03/2022
     */
     getRecommendProducts(params) {
      let urlFull = this.controller + "/recommand-product";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    /**
     * Get product List by Category
     * TTLONG 26/02/2022
     */
     getProductsByCategory(params) {
       const query=`categoryID=${params.categoryID}&queryText=${params.queryText}&sortBy=${params.sortBy}&pageSize=${params.pageSize}&pageIndex=${params.pageIndex}`
      let urlFull = this.controller + `/products-by-category?${query}`;
      return BaseAPIConfig.post(urlFull,params.filter);
    }
    getProductDetailsPaging(params){
      let urlFull = this.controller + "/product-detail-paging";
      return BaseAPIConfig.get(urlFull,{params:params});
    }
    deleteProductDetail(id){
      return BaseAPIConfig.delete(`${this.controller}/delete-product-detail/${id}`);
    }
    warrentyLookup(param){
      return BaseAPIConfig.get(`${this.controller}/warrenty-lookup`,{params:param});
    }
    getProductAttributeByID(param){
      return BaseAPIConfig.get(`${this.controller}/product-attribute-id`,{params:param});
    }

    /**
     * Filter employee
     * TTLONG 20/07/2021
     */
    filter(params) {
      let urlFull =
        this.controller +
        `/productFilter`;
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
  
  export default new ProductAPI();