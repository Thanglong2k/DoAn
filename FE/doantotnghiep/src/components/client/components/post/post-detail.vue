<template>
  <div class="post-detail-container tw-bg-white tw-p-6" v-if="Object.keys(post).length>0">
    <div class="post-header tw-border-b-2 tw-border-solid tw-border-gray-300 tw-pb-3">
        <div class="post-title tw-text-gray-700 tw-text-3xl tw-font-bold">{{post.Post.Title}}</div>
        <div class="post-author tw-text-gray-400">{{post.Account.FullName}} - {{$commonFn.formatDate(new Date(post.Post.CreatedDate),'dd/MM/yyyy hh:mm')}}</div>
    </div>
    <div class="post-description tw-mt-3 tw-border-b-2 tw-border-solid tw-border-gray-300 tw-pb-3 tw-font-semibold">
        {{post.Post.Description}}
    </div>
    <div class="post-content tw-mt-3" v-html="post.Post.Content">
        
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
export default {
    data(){
        return{

        }
    },
    computed:{
        ...mapState('post',['post'])
    },
    created(){
        const me=this
        const postID=me.$route.params.postID
        me.getPostDetailByID({postID:postID})
    },
    methods:{
        ...mapActions('post',['getPostDetailByID'])
    }
}
</script>

<style>

</style>