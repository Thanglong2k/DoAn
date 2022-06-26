import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

const routes = [{
    path: '/control',
    name: 'Control',
    component: () => import('../components/controls'),
    redirect: '',
    children: [{
        path: '/carousel',
        name: 'Carousel',
        component: () => import('../components/controls/product-slider'),
      },
      {
        path: '',
        name: 'Image',
        component: () => import('../components/controls/product-carousel'),
      }
    ]
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../components/admin/components/login/index.vue')

  },
  //ADMIN
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('../components/admin/layouts'),
    redirect: to => {
      return { name: 'DashboardAdmin'}
    },
    children: [{
        path: 'overview',
        name: 'DashboardAdmin',
        component: () => import('../components/admin/views/overviews/index.vue')

      },
      
      {
        path: 'account',
        name: 'Account',
        component: () => import('../components/admin/components/accounts/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'AccountList',
            component: () => import('../components/admin/views/accounts/index.vue'),
          },
          {
            path: 'create-account',
            name: 'CreateAccount',
            component: () => import('../components/admin/views/accounts/popup/create-account.vue'),
          }
        ]

      },
      {
        path: 'product',
        name: 'ProductAdmin',
        component: () => import('../components/admin/components/products/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'ProductList',
            component: () => import('../components/admin/views/products/index.vue'),
          },
          {
            path: 'create-product',
            name: 'CreateProduct',
            component: () => import('../components/admin/views/products/create-product.vue'),
          },
          {
            path: 'imei',
            name: 'IMEI',
            component: () => import('../components/admin/views/products/imei-list.vue'),
          }
        ]

      },
      {
        path: 'manufacturer',
        name: 'Manufacturer',
        component: () => import('../components/admin/components/manufacturers/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'ManufacturerList',
            component: () => import('../components/admin/views/manufacturers/index.vue'),
          },
          // {
          //   path: 'create-manufacturer',
          //   name: 'CreateManufacturer',
          //   component: () => import('../components/admin/views/manufacturers/create-manufacturer.vue'),
          // }
        ]

      },
      {
        path: 'category',
        name: 'Category',
        component: () => import('../components/admin/components/categories/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'CategoryList',
            component: () => import('../components/admin/views/categories/index.vue'),
          },
          // {
          //   path: 'create-manufacturer',
          //   name: 'CreateManufacturer',
          //   component: () => import('../components/admin/views/categories/create-manufacturer.vue'),
          // }
        ]

      },
      {
        path: 'order',
        name: 'Order',
        component: () => import('../components/admin/components/orders/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'OrderList',
            component: () => import('../components/admin/views/orders/index.vue'),
          },
          {
            path: 'detail',
            name: 'OrderDetail',
            component: () => import('../components/admin/views/orders/detail.vue'),
          }
        ]

      },
      {
        path: 'receipt',
        name: 'Receipt',
        component: () => import('../components/admin/components/receipts/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'ReceiptList',
            component: () => import('../components/admin/views/receipts/index.vue'),
          },
          {
            path: 'detail',
            name: 'ReceiptDetail',
            component: () => import('../components/admin/views/receipts/detail.vue'),
          },
          {
            path: 'create-receipt',
            name: 'CreateReceipt',
            component: () => import('../components/admin/views/receipts/create-receipt.vue'),
          }
        ]

      },
      {
        path: 'comment',
        name: 'Comment',
        component: () => import('../components/admin/components/comments/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'CommentList',
            component: () => import('../components/admin/views/comments/index.vue'),
          },
          // {
          //   path: 'detail',
          //   name: 'OrderDetail',
          //   component: () => import('../components/admin/views/receipts/detail.vue'),
          // },
          // {
          //   path: 'create-receipt',
          //   name: 'CreateReceipt',
          //   component: () => import('../components/admin/views/receipts/create-receipt.vue'),
          // }
        ]

      },
      {
        path: 'post',
        name: 'PostAdmin',
        component: () => import('../components/admin/components/post/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'PostList',
            component: () => import('../components/admin/views/post/index.vue'),
          },
          // {
          //   path: 'detail',
          //   name: 'PostDetail',
          //   component: () => import('../components/admin/views/posst/detail.vue'),
          // },
          {
            path: 'create-post',
            name: 'CreatePost',
            component: () => import('../components/admin/views/post/create-post.vue'),
          }
        ]

      },
      {
        path: 'evaluation',
        name: 'Evaluation',
        component: () => import('../components/admin/components/evaluations/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'EvaluationList',
            component: () => import('../components/admin/views/evaluations/index.vue'),
          },
          {
            path: 'detail',
            name: 'OrderDetail',
            component: () => import('../components/admin/views/receipts/detail.vue'),
          }
          
        ]

      },
      
      {
        path: 'customer',
        name: 'Customer',
        component: () => import('../components/admin/components/customers/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'CustomerList',
            component: () => import('../components/admin/views/customers/index.vue'),
          }, 
        ]

      },
      {
        path: 'suplier',
        name: 'Suplier',
        component: () => import('../components/admin/components/supliers/index.vue'),
        redirect:'',
        children: [
          {
            path: '',
            name: 'SuplierList',
            component: () => import('../components/admin/views/supliers/index.vue'),
          }, 
        ]

      },
    ]

  },
  //CLIENT
  {
    path: '/',
    name: 'Client',
    component: () => import('../components/client/layouts'),
    redirect: '',
    children: [{
        path: '',
        name: 'Dashboard',
        component: () => import('../components/client/components/dashboard')

      },
      {
        path: '/product',
        name: 'Product',
        component: () => import('../components/client/components/item-product.vue')

      },
      {
        path: '/warranty-lookup',
        name: 'WarrantyLookup',
        component: () => import('../components/client/components/warranty-lookup')

      },

      {
        path: '/card-item',
        name: 'CardItem',
        component: () => import('../components/controls/item-card')

      },
      {
        path: '/compare-product',
        name: 'CompareProduct',
        component: () => import('../components/client/components/compare-product')

      },
      {
        path: '/product-cart',
        name: 'ProductCart',
        component: () => import('../components/client/components/product-cart')

      },
      {
        path: '/posts',
        name: 'Post',
        component: () => import('../components/client/components/post/post-list.vue'),
      },
      {
        path: '/post/:productMeta/:postID',
        name: 'PostDetail',
        component: () => import('../components/client/components/post/post-detail.vue'),
      },
      // {
      //   path: 'post-detail',
      //   name: 'PostDetail',
      //   component: () => import('../components/client/components/post/post-detail.vue'),
      // },
      {
        path: '/purchase-history',
        name: 'PurchaseHistory',
        component: () => import('../components/client/components/purchase-history')

      },
      {
        path: '/follow-order',
        name: 'FollowOrder',
        component: () => import('../components/client/views/product-cart/follow-order')

      },
      {
        path: '/filter/:category',
        name: 'ListProduct',
        component: () => import('../components/client/components/list-product')

      },
      {
        path: '/search',
        name: 'Search',
        component: () => import('../components/client/components/list-product')

      },
      
      {
        path: '/:productMeta',
        name: 'ProductDetail',
        component: () => import('../components/client/components/product-detail')

      },
    ]
  },

  // {
  //   path: '/',
  //   name: 'Employee',
  //   component: () => import('../components/views/dashboard')
  //  }
  // {
  //   path: '/equipment/em',
  //   name: 'Employeek',
  //   component: () => import('../components/views/Index.vue')
  //  },

]

const router = new VueRouter({
  routes,
  mode: 'history'
})

export default router