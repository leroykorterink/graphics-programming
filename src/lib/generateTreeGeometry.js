export default (minWidth = 4, maxWidth = 8, minHeight = 10, maxHeight = 12) => {
  const width = Math.random() * (maxWidth - minWidth) + minWidth;
  const height = Math.random() * (maxHeight - minHeight) + minHeight;

  const geometry = new THREE.CylinderGeometry(0, width, height, 15, 5);

  return geometry;
};
