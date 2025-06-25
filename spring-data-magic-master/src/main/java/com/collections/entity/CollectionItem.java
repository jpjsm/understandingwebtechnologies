package com.collections.entity;

import java.math.BigDecimal;
import java.util.Set;

import javax.persistence.CascadeType;
import javax.persistence.CollectionTable;
import javax.persistence.Column;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Lob;
import javax.persistence.OneToOne;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name="COLLECTION_ITEMS")
public class CollectionItem {
	
	@Id
	@SequenceGenerator(name = "CollectionItems_Generator", sequenceName = "Collection_Items_seq", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "CollectionItems_Generator")
	long id;

	BigDecimal price;
	
	@OneToOne(cascade={CascadeType.PERSIST, CascadeType.MERGE}, orphanRemoval=true)
	@JoinColumn(name="small_image", unique=true)
	Image smallImage;
	
	@OneToOne(cascade={CascadeType.PERSIST, CascadeType.MERGE}, orphanRemoval=true)
	@JoinColumn(name="image", unique=true)
	Image image;
	
	String name;
	
	String summary;
	
	@Lob
	String description;
	
	Short year;
	
	String country;

	@ElementCollection
	@CollectionTable(name="ITEMS_TOPICS",
			joinColumns=@JoinColumn(name="ITEM_ID"))
	@Column(name="TOPIC")
	Set<String> topics;
	
	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public BigDecimal getPrice() {
		return price;
	}

	public void setPrice(BigDecimal price) {
		this.price = price;
	}

	public Image getSmallImage() {
		return smallImage;
	}

	public void setSmallImage(Image smallImage) {
		this.smallImage = smallImage;
	}

	public Image getImage() {
		return image;
	}

	public void setImage(Image image) {
		this.image = image;
	}

	public void setYear(Short year) {
		this.year = year;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getSummary() {
		return summary;
	}

	public void setSummary(String summary) {
		this.summary = summary;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public Short getYear() {
		return year;
	}

	public void setYear(short year) {
		this.year = year;
	}

	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public Set<String> getTopics() {
		return topics;
	}

	public void setTopics(Set<String> topics) {
		this.topics = topics;
	}
}
