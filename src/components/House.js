import AdvancedComponent from "../core/AdvancedComponent.js";

const House = (
  wallsWidth = 5,
  wallsHeight = 4,
  wallsDepth = 5,
  roofHeight = 3,
  roofOverhead = 0.85
) =>
  class extends AdvancedComponent {
      
    constructor(scene) {
      super();

      const walls = this.wallsFactory(wallsWidth, wallsHeight, wallsDepth);
      const roof = this.roofFactory(roofHeight, roofOverhead, wallsHeight);
      const chimney = this.chimneyFactory(wallsHeight, roofHeight);
      
      const group = new THREE.Group();
      group.add( walls );
      group.add( roof );
      group.add( chimney );

      group.position.set( 0, -2, -5 );

      window.newHouse = group;
      
      scene.add(group);
    }

    wallsFactory(wallsWidth, wallsHeight, wallsDepth) {

      const texture = new THREE.TextureLoader().load( 'assets/materials/stone_wall.jpg' );
      texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
      texture.repeat.set( 5, 5 );

      const wallMaterial = new THREE.MeshBasicMaterial( { map: texture } );

      const walls = new THREE.BoxBufferGeometry(wallsWidth, wallsHeight, wallsDepth);

      const wallsMesh = new THREE.Mesh(walls, wallMaterial)

      return wallsMesh;
    }

    roofFactory(roofHeight, roofOverhead, wallsHeight) {

      // normal roofOverhead = 0.705 

      const roofMaterial = new THREE.MeshBasicMaterial({
        color: 0xaf482e,
      });

      const roof = new THREE.ConeBufferGeometry(wallsWidth * roofOverhead, roofHeight, 4, 1, false);
      const roofMesh = new THREE.Mesh(roof, roofMaterial);

      roofMesh.position.set( 0, (wallsHeight / 2) + (roofHeight / 2), 0 );
      roofMesh.rotation.set( 0, 0.785, 0 );

      return roofMesh;
    }

    chimneyFactory(wallsHeight, roofHeight) {

      const chimneyMaterial = new THREE.MeshBasicMaterial({
        color: 0x7f3026,
      });

      const chimney = new THREE.BoxBufferGeometry(1, 3, 1);
      const chimneyMesh = new THREE.Mesh(chimney, chimneyMaterial);

      //const smoke = new Smoke();

      const chimneyGroup = new THREE.Group();
      chimneyGroup.add( chimneyMesh );
      //chimneyGroup.add( smoke );

      chimneyGroup.position.set( -1, (wallsHeight / 2) + (roofHeight / 2), 0 );

      return chimneyGroup;
    }

    update() {
    }
  };

export default House;
