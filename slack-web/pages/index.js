import MainContent from './components/main-content';
export default function Home() {
  return (
    <div className="container">
      <div className="search">search</div>
      <div className="heading">heading</div>
      <div className="sidebar">side bar</div>
      <MainContent />
      {/* <div className="thread">Thread</div> */}
    </div>
  );
}
