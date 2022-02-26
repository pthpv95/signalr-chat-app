import { useRouter } from "next/router"
import { useState } from "react"
import { login } from "../../pages/api/auth"

const useLogin = (username, password) => {
  const [isLoading, setIsLoading] = useState(false)
  const router = useRouter()
  const mutate = () => {
    setIsLoading(true)
    login(username, password).then((res) => {
      localStorage.setItem('token', res.token)
      router.push('/chat')
    }).catch((err) => {
      alert(err.message)
    }).finally(() => {
      setIsLoading(false)
    })
  }

  return {
    isLoading,
    mutate
  }
}

export default useLogin;