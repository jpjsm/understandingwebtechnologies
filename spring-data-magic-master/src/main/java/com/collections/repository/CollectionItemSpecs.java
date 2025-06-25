package com.collections.repository;

import javax.persistence.criteria.Predicate;
import org.springframework.data.jpa.domain.Specification;

import com.collections.entity.CollectionItem;
import com.collections.entity.CollectionItem_;

public class CollectionItemSpecs {
	
	public static Specification<CollectionItem> getBasic() {
	    return (root, query, cb) -> {
	    	return cb.conjunction();
	    };
	  }
	
	public static Specification<CollectionItem> filterByCountry(String country) {
	    return (root, query, cb) -> {
	    	return cb.equal(root.get(CollectionItem_.country), country);
	    };
	  }
	
	public static Specification<CollectionItem> filterByYear(Short year) {
		return (root, query, cb) -> {
	    	return cb.equal(root.get(CollectionItem_.year), year);
	    };
	  }
	
	public static Specification<CollectionItem> filterByTopics(String ... topics) {
	    return (root, query, cb) -> {
	    	Predicate topicFilter = cb.conjunction();
			for(String topic : topics) {
				topicFilter = cb.and(topicFilter, cb.isMember(topic, root.get(CollectionItem_.topics)));
			}
			return topicFilter;
	    };
	  }
}
