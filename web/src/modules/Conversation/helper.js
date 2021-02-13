const getFullNameAlias = (firstName = "", lastName = "") => {
  const alias = firstName.charAt(0) + lastName.charAt(0)
  return alias.toUpperCase()
}

const getRandomColor = () => {
  const COLORS = ["gray", "orange", "white", "yellow", "cyan", "black"]
  return COLORS[Math.floor(Math.random() * COLORS.length)]
}

export { getFullNameAlias, getRandomColor }
