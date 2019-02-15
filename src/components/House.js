import AdvancedComponent from "../core/AdvancedComponent.js";
import Smoke from "../components/Smoke.js";

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

      this.animationGroup = [];

      const walls = this.wallsFactory(wallsWidth, wallsHeight, wallsDepth);
      const roof = this.roofFactory(roofHeight, roofOverhead, wallsHeight);
      const chimney = this.chimneyFactory(wallsHeight, roofHeight);
      
      const group = new THREE.Group();
      group.add( walls );
      group.add( roof );
      group.add( chimney );

      group.position.set( 0, 2, -10 );

      window.newHouse = group;

      scene.add(group);
    }

    wallsFactory(wallsWidth, wallsHeight, wallsDepth) {

      const texture = new THREE.TextureLoader().load( 'assets/materials/stone_wall.jpg' );
      texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
      texture.repeat.set( 5, 5 );

      const wallMaterial = new THREE.MeshLambertMaterial( { map: texture } );

      const walls = new THREE.BoxBufferGeometry(wallsWidth, wallsHeight, wallsDepth);

      const wallsMesh = new THREE.Mesh(walls, wallMaterial)
      wallsMesh.castShadow = true;
      wallsMesh.receiveShadow = true;

      return wallsMesh;
    }

    roofFactory(roofHeight, roofOverhead, wallsHeight) {
      // normal roofOverhead = 0.705 

      const roofMaterial = new THREE.MeshLambertMaterial({
        color: 0xaf482e,
      });

      const roof = new THREE.ConeBufferGeometry(wallsWidth * roofOverhead, roofHeight, 4, 1, false);
      const roofMesh = new THREE.Mesh(roof, roofMaterial);
      roofMesh.castShadow = true;
      roofMesh.receiveShadow = true;

      roofMesh.position.set( 0, (wallsHeight / 2) + (roofHeight / 2), 0 );
      roofMesh.rotation.set( 0, 0.785, 0 );

      return roofMesh;
    }

    chimneyFactory(wallsHeight, roofHeight) {

      const chimneyMaterial = new THREE.MeshLambertMaterial({
        color: 0x7f3026,
      });

      const chimney = new THREE.BoxBufferGeometry(1, 3, 1);
      const chimneyMesh = new THREE.Mesh(chimney, chimneyMaterial);
      chimneyMesh.castShadow = true;
      chimneyMesh.receiveShadow = true;

      const smoke = this.smokeFactory();

      const chimneyGroup = new THREE.Group();
      chimneyGroup.add( chimneyMesh );
      chimneyGroup.add( smoke );

      chimneyGroup.position.set( -1, (wallsHeight / 2) + (roofHeight / 2), 0 );

      return chimneyGroup;
    }

    smokeFactory() {
      this.smokeMaterial = new THREE.MeshLambertMaterial({
        color: 0xffffff,
        transparent: true
      });
      
      this.smokeMaterial.flatShading = true;
  
      const smoke = new THREE.IcosahedronBufferGeometry(0.2);
  
      this.smokeMesh = new THREE.Mesh(smoke, this.smokeMaterial);
  
      this.resetSmoke(this.smokeMesh, this.smokeMaterial);
  
      return this.smokeMesh;
    }
  
    update() {

      if(this.smokeMaterial.opacity < 0)
        this.resetSmoke(this.smokeMesh, this.smokeMaterial);
      
      const mesh = this.smokeMesh;
  
      mesh.position.set(
        mesh.position.x += 0.003,
        mesh.position.y += 0.003,
        mesh.position.z -= 0.005,
      );
  
      mesh.scale.set(
        mesh.scale.x + 0.003,
        mesh.scale.y + 0.003,
        mesh.scale.z + 0.003,
      );
  
      this.smokeMaterial.opacity -= 0.001; 
    }
  
    resetSmoke(smokeMesh, smokeMaterial) {
      smokeMesh.position.set(0, 1.5, 0);
      smokeMesh.scale.set(1,1,1);
      smokeMaterial.opacity = 1; 
    }
  }

export default House;
