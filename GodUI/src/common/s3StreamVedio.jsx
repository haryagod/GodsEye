//pass key to refresh component on every vedio change
const S3StreamVedio = ({vedioLink,width,height}) => {
    return (
        
    <video width={width} height={height} controls>
  <source src={vedioLink} />
  Your browser does not support the video.
</video>
    );
}
 
export default S3StreamVedio;