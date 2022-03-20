<template>
  <li :class="{'active': isSelected}" @click="() => onClick(contact.id)">
    <div class="d-flex bd-highlight contact">
      <div class="img_cont">
        <img
          src="https://static.turbosquid.com/Preview/001292/481/WV/_D.jpg"
          class="rounded-circle user_img"
          v-if="contact.avatar"
        >
        <div v-if="!contact.avatar" class="user_img_default">{{contact.aka}}
        </div>
        <span class="online_icon"></span>
      </div>
      <div class="user_info">
        <span>{{contact.name}}</span><span v-if="contact.typing" ><img class="typing" :src="typingIcon" ></span>
      </div>
    </div>
  </li>
</template>

<script>
import typingIcon from "../../assets/images/typing-icon.gif";

export default {
  props: {
    isSelected: Boolean,
    contact: Object,
    onClick: Function
  },
  data(){
    return {
      typingIcon,
      isTyping: false
    }
  },
};
</script>

<style>
.typing{
  width: 40px;
  padding: 5px;
}
</style>