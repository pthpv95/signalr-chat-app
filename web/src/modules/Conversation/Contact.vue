<template>
  <li :class="{'active': isSelected}" @click="() => onClick(contact.id)">
    <div class="d-flex bd-highlight contact">
      <div class="img_cont">
        <img
          src="https://static.turbosquid.com/Preview/001292/481/WV/_D.jpg"
          class="rounded-circle user_img"
          v-if="contact.avatar"
        >
        <div v-if="!contact.avatar" class="user_img_default" 
          v-bind:style="defaultAvatarStyleObject">{{avatarUrl}}
        </div>
        <span class="online_icon"></span>
      </div>
      <div class="user_info">
        <span>{{fullName}}</span><span v-if="contact.typing" ><img class="typing" :src="typingIcon" ></span>
        <!-- <p>{{contact.firstName }} {{contact.lastName}} is online</p> -->
      </div>
    </div>
  </li>
</template>

<script>
import {getFullNameAlias, getRandomColor } from './helper';
import typingIcon from "../../assets/images/typing-icon.gif";

export default {
  props: {
    isSelected: Boolean,
    contact: Object,
    onClick: Function
  },
  data(){
    return {
      defaultAvatarStyleObject: {
        backgroundColor: getRandomColor()
      },
      typingIcon,
      isTyping: false
    }
  },
  computed: {
    avatarUrl(){
      const firstName = this.contact.firstName || '';
      const lastName = this.contact.lastName || '';
      return getFullNameAlias(firstName, lastName)
    },
    fullName(){
      return this.contact.firstName + ' ' + this.contact.lastName;
    },
  },
  watch: {
    contact:{
      immediate: true,
      handler(val, oldVal){
        // console.log(val);
      }
    }
  }
};
</script>

<style>
.typing{
  width: 40px;
  padding: 5px;
}
</style>